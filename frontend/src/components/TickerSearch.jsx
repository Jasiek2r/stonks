import React, { useState } from 'react';
import { Container, Form, Button, Card, Alert, Table, Spinner } from 'react-bootstrap';
import axios from 'axios';
import ChartsView from '../ChartsView';

// Dane zapasowe na wypadek problemów z API
const FALLBACK_RESULTS = [
    {
        symbol: "AAPL",
        name: "Apple Inc",
        type: "Equity",
        region: "United States",
        currency: "USD",
        marketOpen: "09:30",
        marketClose: "16:00",
        timezone: "UTC-04"
    },
    {
        symbol: "MSFT",
        name: "Microsoft Corporation",
        type: "Equity",
        region: "United States",
        currency: "USD",
        marketOpen: "09:30",
        marketClose: "16:00",
        timezone: "UTC-04"
    },
    {
        symbol: "GOOGL",
        name: "Alphabet Inc",
        type: "Equity",
        region: "United States",
        currency: "USD",
        marketOpen: "09:30",
        marketClose: "16:00",
        timezone: "UTC-04"
    }
];

const TickerSearch = () => {
    const [searchQuery, setSearchQuery] = useState('');
    const [results, setResults] = useState([]);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);
    const [selectedTicker, setSelectedTicker] = useState(null);
    const [debugInfo, setDebugInfo] = useState(null);

    const handleSearch = async (e) => {
        e.preventDefault();
        if (!searchQuery.trim()) {
            setError('Wprowadź tekst do wyszukiwania');
            return;
        }

        setIsLoading(true);
        setError(null);
        setResults([]);
        setSelectedTicker(null);
        setDebugInfo(null);

        try {
            console.log('Wysyłanie zapytania:', searchQuery);
            const response = await axios.get(`http://localhost:5125/api/search?query=${encodeURIComponent(searchQuery)}`);
            console.log('Otrzymane wyniki:', response.data);
            
            setDebugInfo({
                status: response.status,
                statusText: response.statusText,
                headers: response.headers,
                data: response.data
            });

            if (!response.data || response.data.length === 0) {
                // Jeśli nie ma wyników z API, użyj danych zapasowych które pasują do wyszukiwania
                const fallbackResults = FALLBACK_RESULTS.filter(item => 
                    item.symbol.toLowerCase().includes(searchQuery.toLowerCase()) ||
                    item.name.toLowerCase().includes(searchQuery.toLowerCase())
                );
                
                if (fallbackResults.length > 0) {
                    setResults(fallbackResults);
                    setError('Używam danych zapasowych - API może być chwilowo niedostępne');
                } else {
                    setError('Nie znaleziono wyników');
                }
                return;
            }
            
            setResults(response.data);
        } catch (err) {
            console.error('Błąd wyszukiwania:', err);
            setDebugInfo({
                error: err.message,
                response: err.response?.data,
                status: err.response?.status
            });

            // W przypadku błędu API, również spróbuj użyć danych zapasowych
            const fallbackResults = FALLBACK_RESULTS.filter(item => 
                item.symbol.toLowerCase().includes(searchQuery.toLowerCase()) ||
                item.name.toLowerCase().includes(searchQuery.toLowerCase())
            );
            
            if (fallbackResults.length > 0) {
                setResults(fallbackResults);
                setError(`Błąd API (${err.message}). Używam danych zapasowych.`);
            } else {
                setError(`Błąd wyszukiwania: ${err.response?.data || err.message}`);
            }
        } finally {
            setIsLoading(false);
        }
    };

    const handleTickerSelect = (ticker) => {
        setSelectedTicker(ticker);
    };

    return (
        <Container fluid className="mt-4">
            {/* Wyszukiwarka */}
            <Card className="mb-4">
                <Card.Header as="h5" className="text-center bg-primary text-white">
                    Wyszukiwarka Spółek
                </Card.Header>
                <Card.Body>
                    <Form onSubmit={handleSearch}>
                        <Form.Group className="mb-3">
                            <Form.Label>Wyszukaj spółkę</Form.Label>
                            <div className="d-flex">
                                <Form.Control
                                    type="text"
                                    placeholder="Wpisz nazwę lub symbol spółki..."
                                    value={searchQuery}
                                    onChange={(e) => setSearchQuery(e.target.value)}
                                />
                                <Button 
                                    variant="primary" 
                                    type="submit" 
                                    className="ms-2"
                                    disabled={isLoading}
                                >
                                    {isLoading ? (
                                        <>
                                            <Spinner
                                                as="span"
                                                animation="border"
                                                size="sm"
                                                role="status"
                                                aria-hidden="true"
                                            />
                                            <span className="ms-2">Szukam...</span>
                                        </>
                                    ) : (
                                        'Szukaj'
                                    )}
                                </Button>
                            </div>
                        </Form.Group>
                    </Form>

                    {error && (
                        <Alert variant={error.includes('danych zapasowych') ? 'warning' : 'danger'} className="mt-3">
                            {error}
                        </Alert>
                    )}

                    {debugInfo && (
                        <Alert variant="info" className="mt-3">
                            <details>
                                <summary>Informacje diagnostyczne</summary>
                                <pre>{JSON.stringify(debugInfo, null, 2)}</pre>
                            </details>
                        </Alert>
                    )}

                    {results.length > 0 && (
                        <Table responsive striped hover className="mt-3">
                            <thead>
                                <tr>
                                    <th>Symbol</th>
                                    <th>Nazwa</th>
                                    <th>Region</th>
                                    <th>Waluta</th>
                                    <th>Typ</th>
                                    <th>Godziny handlu</th>
                                </tr>
                            </thead>
                            <tbody>
                                {results.map((result, index) => (
                                    <tr 
                                        key={index}
                                        onClick={() => handleTickerSelect(result.symbol)}
                                        style={{ cursor: 'pointer' }}
                                        className={selectedTicker === result.symbol ? 'table-primary' : ''}
                                    >
                                        <td><strong>{result.symbol}</strong></td>
                                        <td>{result.name}</td>
                                        <td>{result.region}</td>
                                        <td>{result.currency}</td>
                                        <td>{result.type}</td>
                                        <td>{result.marketOpen} - {result.marketClose} ({result.timezone})</td>
                                    </tr>
                                ))}
                            </tbody>
                        </Table>
                    )}

                    {results.length === 0 && !isLoading && !error && searchQuery && (
                        <Alert variant="info" className="mt-3">
                            Nie znaleziono wyników dla "{searchQuery}"
                        </Alert>
                    )}
                </Card.Body>
            </Card>

            {/* Wykresy */}
            {selectedTicker && (
                <Card>
                    <Card.Header as="h5" className="text-center bg-primary text-white">
                        Szczegółowe dane dla {selectedTicker}
                    </Card.Header>
                    <Card.Body>
                        <ChartsView initialTicker={selectedTicker} />
                    </Card.Body>
                </Card>
            )}
        </Container>
    );
};

export default TickerSearch; 