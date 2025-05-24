import React, { useState } from 'react';
import { Container, Form, Button, Card, Alert, Row, Col } from 'react-bootstrap';
import axios from 'axios';

// Configure axios defaults
axios.defaults.baseURL = 'http://localhost:5125'; // Updated backend port
axios.defaults.headers.common['Accept'] = 'application/json';
axios.defaults.headers.post['Content-Type'] = 'application/json';

const CurrencyCalculator = () => {
    const [formData, setFormData] = useState({
        fromCurrency: 'PLN',
        toCurrency: 'EUR',
        amount: ''
    });
    const [result, setResult] = useState(null);
    const [error, setError] = useState(null);
    const [isLoading, setIsLoading] = useState(false);

    const currencies = [
        { code: 'PLN', name: 'Polski Złoty' },
        { code: 'EUR', name: 'Euro' },
        { code: 'USD', name: 'Dolar Amerykański' },
        { code: 'GBP', name: 'Funt Brytyjski' },
        { code: 'CHF', name: 'Frank Szwajcarski' },
        { code: 'JPY', name: 'Jen Japoński' },
        { code: 'CZK', name: 'Korona Czeska' },
        { code: 'NOK', name: 'Korona Norweska' },
        { code: 'SEK', name: 'Korona Szwedzka' },
        { code: 'DKK', name: 'Korona Duńska' },
        { code: 'CAD', name: 'Dolar Kanadyjski' },
        { code: 'AUD', name: 'Dolar Australijski' },
        { code: 'HUF', name: 'Forint Węgierski' },
        { code: 'RON', name: 'Lej Rumuński' },
        { code: 'BGN', name: 'Lew Bułgarski' },
        { code: 'TRY', name: 'Lira Turecka' },
        { code: 'ILS', name: 'Nowy Szekel Izraelski' },
        { code: 'CNY', name: 'Yuan Chiński' }
    ];

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        
        if (name === 'amount') {
            // Allow empty value or positive numbers with up to 2 decimal places
            if (value === '' || (/^\d*\.?\d{0,2}$/.test(value) && parseFloat(value) >= 0)) {
                setFormData(prev => ({
                    ...prev,
                    [name]: value
                }));
            }
        } else {
            setFormData(prev => ({
                ...prev,
                [name]: value
            }));
        }

        // Clear previous results and errors when input changes
        setError(null);
        setResult(null);
    };

    const handleSwapCurrencies = () => {
        setFormData(prev => ({
            ...prev,
            fromCurrency: prev.toCurrency,
            toCurrency: prev.fromCurrency
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError(null);
        setResult(null);
        setIsLoading(true);

        // Validate amount
        const amount = parseFloat(formData.amount);
        if (isNaN(amount) || amount <= 0) {
            setError('Proszę wprowadzić prawidłową kwotę większą od 0');
            setIsLoading(false);
            return;
        }

        try {
            console.log('Sending request to:', `${axios.defaults.baseURL}/api/currency/calculate`);
            console.log('Request data:', {
                fromCurrency: formData.fromCurrency,
                toCurrency: formData.toCurrency,
                amount: amount
            });

            const response = await axios.post('/api/currency/calculate', {
                fromCurrency: formData.fromCurrency,
                toCurrency: formData.toCurrency,
                amount: amount
            });

            console.log('API Response:', response.data);

            if (response.data) {
                setResult({
                    fromCurrency: response.data.fromCurrency,
                    toCurrency: response.data.toCurrency,
                    amount: parseFloat(response.data.amount),
                    convertedAmount: parseFloat(response.data.convertedAmount),
                    exchangeRate: parseFloat(response.data.exchangeRate),
                    timestamp: new Date(response.data.timestamp)
                });
            } else {
                setError('Nie otrzymano danych z serwera');
            }
        } catch (err) {
            console.error('API Error:', err);
            if (err.response) {
                console.error('Error response:', err.response);
                setError(`Błąd: ${err.response.status} - ${err.response.data || 'Nieznany błąd'}`);
            } else if (err.request) {
                console.error('Error request:', err.request);
                setError('Nie można połączyć się z serwerem. Sprawdź czy backend jest uruchomiony.');
            } else {
                setError('Wystąpił błąd podczas przeliczania walut');
            }
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <Container className="mt-4">
            <Card>
                <Card.Header as="h5" className="text-center bg-primary text-white">
                    Kalkulator Walut
                </Card.Header>
                <Card.Body>
                    <Form onSubmit={handleSubmit}>
                        <Row className="align-items-end">
                            <Col md={4}>
                                <Form.Group className="mb-3">
                                    <Form.Label>Z waluty</Form.Label>
                                    <Form.Select
                                        name="fromCurrency"
                                        value={formData.fromCurrency}
                                        onChange={handleInputChange}
                                    >
                                        {currencies.map(currency => (
                                            <option key={currency.code} value={currency.code}>
                                                {currency.code} - {currency.name}
                                            </option>
                                        ))}
                                    </Form.Select>
                                </Form.Group>
                            </Col>
                            <Col md={1} className="text-center mb-3">
                                <Button 
                                    variant="outline-primary" 
                                    onClick={handleSwapCurrencies}
                                    type="button"
                                    title="Zamień waluty miejscami"
                                >
                                    ⇄
                                </Button>
                            </Col>
                            <Col md={4}>
                                <Form.Group className="mb-3">
                                    <Form.Label>Na walutę</Form.Label>
                                    <Form.Select
                                        name="toCurrency"
                                        value={formData.toCurrency}
                                        onChange={handleInputChange}
                                    >
                                        {currencies.map(currency => (
                                            <option key={currency.code} value={currency.code}>
                                                {currency.code} - {currency.name}
                                            </option>
                                        ))}
                                    </Form.Select>
                                </Form.Group>
                            </Col>
                            <Col md={3}>
                                <Form.Group className="mb-3">
                                    <Form.Label>Kwota</Form.Label>
                                    <Form.Control
                                        type="text"
                                        name="amount"
                                        value={formData.amount}
                                        onChange={handleInputChange}
                                        placeholder="Wprowadź kwotę"
                                        required
                                    />
                                </Form.Group>
                            </Col>
                        </Row>

                        <div className="d-grid gap-2">
                            <Button 
                                variant="primary" 
                                type="submit"
                                disabled={isLoading || !formData.amount}
                            >
                                {isLoading ? 'Przeliczanie...' : 'Przelicz'}
                            </Button>
                        </div>
                    </Form>

                    {error && (
                        <Alert variant="danger" className="mt-3">
                            {error}
                        </Alert>
                    )}

                    {result && (
                        <Card className="mt-3 border-success">
                            <Card.Body>
                                <h6 className="text-success">Wynik przeliczenia</h6>
                                <div className="d-flex justify-content-center align-items-center my-3">
                                    <h4 className="mb-0">
                                        {result.amount.toFixed(2)} {result.fromCurrency} = {' '}
                                        <strong>{result.convertedAmount.toFixed(2)} {result.toCurrency}</strong>
                                    </h4>
                                </div>
                                <div className="text-center text-muted">
                                    <small>
                                        Kurs: 1 {result.fromCurrency} = {result.exchangeRate.toFixed(4)} {result.toCurrency}
                                        <br />
                                        Aktualizacja: {result.timestamp.toLocaleString('pl-PL')}
                                    </small>
                                </div>
                            </Card.Body>
                        </Card>
                    )}
                </Card.Body>
            </Card>
        </Container>
    );
};

export default CurrencyCalculator; 