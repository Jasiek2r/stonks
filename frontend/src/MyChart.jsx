import React from "react";
import TradingViewWidget from "react-tradingview-widget";

const symbols = ["AAPL", "GOOGL", "TSLA", "AMZN", "MSFT", "NVDA"];

const ChartsView = () => {
    return (
        <div className="d-flex flex-wrap justify-content-center gap-4">
            {symbols.map((symbol) => (
                <div key={symbol} style={{ width: "400px", height: "300px" }}>
                    <TradingViewWidget
                        symbol={symbol}
                        theme="light"
                        locale="en"
                        autosize
                    />
                </div>
            ))}
        </div>
    );
};

export default ChartsView;
