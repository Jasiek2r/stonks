import React, { useEffect, useState } from "react";
import PropTypes from 'prop-types';
import { Card, Spinner, Row, Col, Pagination } from 'react-bootstrap';

const ChartsView = ({ initialTicker = null, showMultiple = false, initialSymbols = [], onSymbolsChange = () => {} }) => {
    const [symbols, setSymbols] = useState(initialTicker ? [initialTicker] : (initialSymbols.length > 0 ? initialSymbols : ["AAPL", "TSLA", "GOOGL", "AMZN", "MSFT"]));
    const [isLoading, setIsLoading] = useState(false);
    const [currentPage, setCurrentPage] = useState(1);
    const itemsPerPage = 5;

    useEffect(() => {
        if (initialTicker) {
            setSymbols([initialTicker]);
            onSymbolsChange([initialTicker]);
        } else if (initialSymbols.length > 0) {
            setSymbols(initialSymbols);
            onSymbolsChange(initialSymbols);
        }
    }, [initialTicker, initialSymbols, onSymbolsChange]);

    if (isLoading) return (
        <div className="text-center p-4">
            <Spinner animation="border" variant="primary" />
            <p className="mt-2">Ładowanie wykresów...</p>
        </div>
    );

    const totalPages = Math.ceil(symbols.length / itemsPerPage);
    const startIndex = (currentPage - 1) * itemsPerPage;
    const visibleSymbols = symbols.slice(startIndex, startIndex + itemsPerPage);

    return (
        <div className="charts-container">
            {showMultiple && symbols.length > itemsPerPage && (
                <div className="d-flex justify-content-center mb-4">
                    <Pagination>
                        <Pagination.First onClick={() => setCurrentPage(1)} disabled={currentPage === 1} />
                        <Pagination.Prev onClick={() => setCurrentPage(prev => Math.max(1, prev - 1))} disabled={currentPage === 1} />
                        {Array.from({ length: totalPages }, (_, i) => i + 1)
                            .filter(page => Math.abs(page - currentPage) <= 2 || page === 1 || page === totalPages)
                            .map((page, index, array) => {
                                if (index > 0 && array[index - 1] !== page - 1) {
                                    return [
                                        <Pagination.Ellipsis key={`ellipsis-${page}`} />,
                                        <Pagination.Item
                                            key={page}
                                            active={page === currentPage}
                                            onClick={() => setCurrentPage(page)}
                                        >
                                            {page}
                                        </Pagination.Item>
                                    ];
                                }
                                return (
                                    <Pagination.Item
                                        key={page}
                                        active={page === currentPage}
                                        onClick={() => setCurrentPage(page)}
                                    >
                                        {page}
                                    </Pagination.Item>
                                );
                            })}
                        <Pagination.Next onClick={() => setCurrentPage(prev => Math.min(totalPages, prev + 1))} disabled={currentPage === totalPages} />
                        <Pagination.Last onClick={() => setCurrentPage(totalPages)} disabled={currentPage === totalPages} />
                    </Pagination>
                </div>
            )}
            <Row>
                {visibleSymbols.map((symbol) => (
                    <Col key={symbol} xs={12} className="mb-4">
                        <Card>
                            <Card.Header className="bg-primary text-white d-flex justify-content-between align-items-center">
                                <h5 className="mb-0">Wykres {symbol}</h5>
                            </Card.Header>
                            <Card.Body className="p-0" style={{ height: '600px' }}>
                                <div
                                    key={`tradingview_${symbol}`}
                                    id={`tradingview_${symbol}`}
                                    style={{ height: '100%', width: '100%' }}
                                >
                                    <iframe
                                        key={symbol}
                                        title={`TradingView ${symbol}`}
                                        style={{ width: '100%', height: '100%', border: 'none' }}
                                        src={`https://s.tradingview.com/widgetembed/?frameElementId=tradingview_${symbol}&symbol=${symbol}&interval=D&hidesidetoolbar=0&symboledit=1&saveimage=1&toolbarbg=f1f3f6&studies=%5B%5D&theme=light&style=1&timezone=exchange&withdateranges=1&showpopupbutton=1&studies_overrides=%7B%7D&overrides=%7B%7D&enabled_features=%5B%5D&disabled_features=%5B%5D&showpopupbutton=1&locale=pl&utm_source=&utm_medium=widget&utm_campaign=chart&utm_term=${symbol}`}
                                    />
                                </div>
                            </Card.Body>
                        </Card>
                    </Col>
                ))}
            </Row>
        </div>
    );
};

ChartsView.propTypes = {
    initialTicker: PropTypes.string,
    showMultiple: PropTypes.bool,
    initialSymbols: PropTypes.arrayOf(PropTypes.string),
    onSymbolsChange: PropTypes.func
};

export default ChartsView;
