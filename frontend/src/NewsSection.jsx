import React, { useEffect, useState } from 'react';
import { Card, Row, Col, Spinner, Alert } from 'react-bootstrap';
import axios from 'axios';

const NewsSection = ({ tickers }) => {
    const [news, setNews] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchNews = async () => {
            try {
                setIsLoading(true);
                setError(null);
                const params = new URLSearchParams();
                if (tickers) {
                    params.append('tickers', tickers);
                }
                
                const url = `http://localhost:5125/api/news?${params}`;
                console.log('Fetching news from:', url);
                
                const response = await axios.get(url);
                console.log('News API response:', response.data);
                
                if (response.data?.items?.length > 0) {
                    setNews(response.data.items);
                } else {
                    console.log('No news items in response');
                    setError('Brak dostępnych wiadomości');
                }
            } catch (err) {
                console.error('Error fetching news:', err);
                setError(err.response?.data?.error || 'Nie udało się pobrać wiadomości');
            } finally {
                setIsLoading(false);
            }
        };

        console.log('NewsSection mounted/updated with tickers:', tickers);
        fetchNews();
    }, [tickers]);

    if (isLoading) {
        return (
            <div className="text-center p-4">
                <Spinner animation="border" variant="primary" />
                <p className="mt-2">Ładowanie wiadomości...</p>
            </div>
        );
    }

    if (error) {
        return (
            <Alert variant="danger">
                {error}
            </Alert>
        );
    }

    if (!news.length) {
        return (
            <Alert variant="info">
                Brak dostępnych wiadomości dla wybranych spółek.
            </Alert>
        );
    }

    return (
        <div className="news-section mb-4">
            <h2 className="mb-4">Najnowsze wiadomości rynkowe</h2>
            <Row xs={1} md={2} lg={3} className="g-4">
                {news.map((item, idx) => (
                    <Col key={idx}>
                        <Card className="h-100">
                            {item.bannerImage && (
                                <Card.Img 
                                    variant="top" 
                                    src={item.bannerImage}
                                    alt={item.title}
                                    style={{ height: '200px', objectFit: 'cover' }}
                                />
                            )}
                            <Card.Body>
                                <Card.Title>{item.title}</Card.Title>
                                <Card.Subtitle className="mb-2 text-muted">
                                    {new Date(item.timePublished).toLocaleString('pl-PL')} | {item.source}
                                </Card.Subtitle>
                                <Card.Text>{item.summary}</Card.Text>
                                <div className="d-flex justify-content-between align-items-center">
                                    <a href={item.url} target="_blank" rel="noopener noreferrer" className="btn btn-primary">
                                        Czytaj więcej
                                    </a>
                                    {item.sentimentLabel && (
                                        <span className={`badge bg-${getSentimentColor(item.sentimentLabel)}`}>
                                            {translateSentiment(item.sentimentLabel)}
                                        </span>
                                    )}
                                </div>
                            </Card.Body>
                        </Card>
                    </Col>
                ))}
            </Row>
        </div>
    );
};

const getSentimentColor = (sentiment) => {
    switch (sentiment.toLowerCase()) {
        case 'bullish':
            return 'success';
        case 'bearish':
            return 'danger';
        case 'neutral':
            return 'secondary';
        default:
            return 'info';
    }
};

const translateSentiment = (sentiment) => {
    switch (sentiment.toLowerCase()) {
        case 'bullish':
            return 'Pozytywny';
        case 'bearish':
            return 'Negatywny';
        case 'neutral':
            return 'Neutralny';
        default:
            return sentiment;
    }
};

export default NewsSection; 