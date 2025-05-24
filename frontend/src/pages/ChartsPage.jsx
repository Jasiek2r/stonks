import React, { useState } from 'react';
import { Container } from 'react-bootstrap';
import SearchBar from '../SearchBar';
import ChartsView from '../ChartsView';

const ChartsPage = () => {
    const [activeSymbols, setActiveSymbols] = useState(["AAPL", "TSLA", "GOOGL", "AMZN", "MSFT"]);

    const handleSymbolsChange = (symbols) => {
        setActiveSymbols(symbols);
    };

    return (
        <Container fluid className="p-4">
            <h1 className="text-center mb-4">Wykresy</h1>
            <SearchBar onSymbolsChange={handleSymbolsChange} />
            <ChartsView 
                showMultiple={true} 
                initialSymbols={activeSymbols} 
                onSymbolsChange={handleSymbolsChange} 
            />
        </Container>
    );
};

export default ChartsPage; 