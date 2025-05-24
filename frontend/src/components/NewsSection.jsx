import React, { useState, useEffect } from 'react';
import { Container, Row, Col, Form, Button, Spinner, Alert } from 'react-bootstrap';
import axios from 'axios';
import NewsCard from './NewsCard';

const NewsSection = () => {
    const [news, setNews] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);
    const [selectedSymbols, setSelectedSymbols] = useState(['AAPL', 'TSLA', 'GOOGL']);
    const [customSymbol, setCustomSymbol] = useState('');
    const [topics, setTopics] = useState('technology,earnings');

    const fetchNews = async () => {
        try {
            setIsLoading(true);
            setError(null);
            
            const params = new URLSearchParams();
            if (selectedSymbols.length > 0) {
                params.append('tickers', selectedSymbols.join(','));
            }
            if (topics) {
                params.append('topics', topics);
            }

            console.log('Fetching news with params:', params.toString());
            const response = await axios.get(`http://localhost:5125/api/news?${params}`);
            
            if (response.data?.items?.length > 0) {
                setNews(response.data.items);
            } else {
                setError('Brak dostępnych wiadomości dla wybranych parametrów');
            }
        } catch (err) {
            console.error('Error fetching news:', err);
            setError(err.response?.data?.error || 'Nie udało się pobrać wiadomości');
        } finally {
            setIsLoading(false);
        }
    };

    useEffect(() => {
        fetchNews();
    }, [selectedSymbols, topics]);

    const handleAddSymbol = (e) => {
        e.preventDefault();
        if (customSymbol && !selectedSymbols.includes(customSymbol.toUpperCase())) {
            setSelectedSymbols([...selectedSymbols, customSymbol.toUpperCase()]);
            setCustomSymbol('');
        }
    };

    const handleRemoveSymbol = (symbol) => {
        setSelectedSymbols(selectedSymbols.filter(s => s !== symbol));
    };

    const topicOptions = [
        { value: 'blockchain', label: 'Blockchain' },
        { value: 'earnings', label: 'Wyniki finansowe' },
        { value: 'ipo', label: 'IPO' },
        { value: 'mergers_and_acquisitions', label: 'Fuzje i przejęcia' },
        { value: 'financial_markets', label: 'Rynki finansowe' },
        { value: 'economy_fiscal', label: 'Ekonomia' },
        { value: 'economy_monetary', label: 'Polityka monetarna' },
        { value: 'technology', label: 'Technologia' }
    ];

    return (
        <Container className="py-4">
            <h2 className="mb-4">Wiadomości Rynkowe</h2>
            
            <Row className="mb-4">
                <Col md={6}>
                    <Form onSubmit={handleAddSymbol}>
                        <Form.Group className="mb-3">
                            <Form.Label>Dodaj symbol spółki</Form.Label>
                            <div className="d-flex">
                                <Form.Control
                                    type="text"
                                    placeholder="np. AAPL"
                                    value={customSymbol}
                                    onChange={(e) => setCustomSymbol(e.target.value)}
                                    className="me-2"
                                />
                                <Button type="submit" variant="primary">
                                    Dodaj
                                </Button>
                            </div>
                        </Form.Group>
                    </Form>

                    <div className="mb-3">
                        <div className="d-flex flex-wrap gap-2">
                            {selectedSymbols.map(symbol => (
                                <span key={symbol} className="badge bg-primary d-flex align-items-center">
                                    {symbol}
                                    <button
                                        className="btn btn-link text-white p-0 ms-2"
                                        onClick={() => handleRemoveSymbol(symbol)}
                                        style={{ fontSize: '1.2em', textDecoration: 'none' }}
                                    >
                                        ×
                                    </button>
                                </span>
                            ))}
                        </div>
                    </div>
                </Col>

                <Col md={6}>
                    <Form.Group className="mb-3">
                        <Form.Label>Filtruj po tematach</Form.Label>
                        <Form.Select 
                            multiple 
                            value={topics.split(',')}
                            onChange={(e) => {
                                const selectedOptions = Array.from(e.target.selectedOptions, option => option.value);
                                setTopics(selectedOptions.join(','));
                            }}
                            style={{ height: '150px' }}
                        >
                            {topicOptions.map(topic => (
                                <option key={topic.value} value={topic.value}>
                                    {topic.label}
                                </option>
                            ))}
                        </Form.Select>
                    </Form.Group>
                </Col>
            </Row>

            {isLoading ? (
                <div className="text-center p-4">
                    <Spinner animation="border" variant="primary" />
                    <p className="mt-2">Ładowanie wiadomości...</p>
                </div>
            ) : error ? (
                <Alert variant="danger">{error}</Alert>
            ) : (
                <Row xs={1} md={2} lg={3} className="g-4">
                    {news.map((item, idx) => (
                        <Col key={idx}>
                            <NewsCard
                                title={item.title}
                                summary={item.summary}
                                source={item.source}
                                publishDate={item.timePublished}
                                url={item.url}
                                sentiment={item.sentimentScore}
                                image={item.bannerImage}
                            />
                        </Col>
                    ))}
                </Row>
            )}
        </Container>
    );
};

export default NewsSection; 