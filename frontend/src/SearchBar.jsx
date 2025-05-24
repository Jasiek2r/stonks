import React, { useState } from 'react';
import PropTypes from 'prop-types';
import { Form, Button, InputGroup } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';

const SearchBar = ({ onSymbolsChange }) => {
    const [searchTerm, setSearchTerm] = useState('');
    const navigate = useNavigate();

    const handleSubmit = (e) => {
        e.preventDefault();
        if (searchTerm.trim()) {
            const symbols = searchTerm.split(',').map(s => s.trim().toUpperCase());
            onSymbolsChange(symbols);
        }
    };

    return (
        <div className="mb-4">
            <div className="d-flex gap-3 align-items-start">
                <Form onSubmit={handleSubmit} className="flex-grow-1">
                    <InputGroup>
                        <Form.Control
                            type="text"
                            placeholder="Wpisz symbole giełdowe (np. AAPL, MSFT, GOOGL)"
                            value={searchTerm}
                            onChange={(e) => setSearchTerm(e.target.value)}
                        />
                        <Button variant="primary" type="submit">
                            Szukaj
                        </Button>
                    </InputGroup>
                    <Form.Text className="text-muted">
                        Wpisz symbole oddzielone przecinkami, np. AAPL, MSFT, GOOGL
                    </Form.Text>
                </Form>
                <Button 
                    variant="primary" 
                    onClick={() => navigate('/news')}
                    className="px-4 py-2"
                    style={{ minWidth: '140px' }}
                >
                    Newsy
                </Button>
            </div>
        </div>
    );
};

SearchBar.propTypes = {
    onSymbolsChange: PropTypes.func.isRequired
};

export default SearchBar; 