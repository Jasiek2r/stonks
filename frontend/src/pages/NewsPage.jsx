import React from 'react';
import { Container } from 'react-bootstrap';

const NewsPage = () => {
    return (
        <Container fluid className="p-4">
            <div 
                className="tradingview-widget-container"
                style={{ height: 'calc(100vh - 200px)' }}
            >
                <iframe
                    title="TradingView News Feed"
                    src="https://s.tradingview.com/embed-widget/timeline/?locale=pl#%7B%22colorTheme%22%3A%22light%22%2C%22isTransparent%22%3Atrue%2C%22displayMode%22%3A%22compact%22%2C%22width%22%3A%22100%25%22%2C%22height%22%3A%22100%25%22%2C%22symbol%22%3A%22NASDAQ%3AAAPL%22%2C%22newsCategory%22%3A%22headline%22%2C%22importanceFilter%22%3A%22-1%2C0%2C1%22%7D"
                    style={{
                        width: '100%',
                        height: '100%',
                        border: 'none'
                    }}
                />
            </div>
        </Container>
    );
};

export default NewsPage; 