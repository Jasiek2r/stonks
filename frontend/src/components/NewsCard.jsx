import React from 'react';
import { Card, Badge } from 'react-bootstrap';
import PropTypes from 'prop-types';

const NewsCard = ({ title, summary, source, publishDate, url, sentiment, image }) => {
    const getSentimentColor = (sentiment) => {
        if (!sentiment) return 'secondary';
        const value = parseFloat(sentiment);
        if (value > 0.3) return 'success';
        if (value < -0.3) return 'danger';
        return 'warning';
    };

    const getSentimentText = (sentiment) => {
        if (!sentiment) return 'Neutralny';
        const value = parseFloat(sentiment);
        if (value > 0.3) return 'Pozytywny';
        if (value < -0.3) return 'Negatywny';
        return 'Neutralny';
    };

    const formatDate = (dateString) => {
        const date = new Date(dateString);
        return date.toLocaleString('pl-PL', {
            year: 'numeric',
            month: 'long',
            day: 'numeric',
            hour: '2-digit',
            minute: '2-digit'
        });
    };

    return (
        <Card className="h-100 shadow-sm">
            {image && (
                <Card.Img 
                    variant="top" 
                    src={image} 
                    style={{ 
                        height: '200px', 
                        objectFit: 'cover',
                        objectPosition: 'center'
                    }}
                />
            )}
            <Card.Body className="d-flex flex-column">
                <div className="d-flex justify-content-between align-items-start mb-2">
                    <Badge bg={getSentimentColor(sentiment)} className="mb-2">
                        {getSentimentText(sentiment)}
                    </Badge>
                    <small className="text-muted">
                        {formatDate(publishDate)}
                    </small>
                </div>
                <Card.Title>{title}</Card.Title>
                <Card.Text className="text-muted mb-3">
                    {summary}
                </Card.Text>
                <div className="mt-auto">
                    <div className="d-flex justify-content-between align-items-center">
                        <small className="text-muted">Źródło: {source}</small>
                        <a 
                            href={url} 
                            target="_blank" 
                            rel="noopener noreferrer" 
                            className="btn btn-outline-primary btn-sm"
                        >
                            Czytaj więcej
                        </a>
                    </div>
                </div>
            </Card.Body>
        </Card>
    );
};

NewsCard.propTypes = {
    title: PropTypes.string.isRequired,
    summary: PropTypes.string.isRequired,
    source: PropTypes.string.isRequired,
    publishDate: PropTypes.string.isRequired,
    url: PropTypes.string.isRequired,
    sentiment: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
    image: PropTypes.string
};

export default NewsCard; 