import React, { useState } from "react";
import ChartsView from "./ChartsView";
import CurrencyCalculator from "./components/CurrencyCalculator";
import TickerSearch from "./components/TickerSearch";
import NewsPage from "./pages/NewsPage";
import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";

import Navbar from "react-bootstrap/Navbar";
import Button from "react-bootstrap/Button";
import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";

const StockData = [
    { symbol: "AAPL", price: 150, min: 100, max: 200 },
    { symbol: "GOOGL", price: 2000, min: 1500, max: 2500 },
    { symbol: "TSLA", price: 700, min: 500, max: 800 },
    { symbol: "AMZN", price: 3000, min: 2500, max: 3500 },
];

function App() {
    const [view, setView] = useState("home");

    const renderView = () => {
        switch (view) {
            case "table":
                return <StockTable stockData={StockData} />;
            case "chartsview":
                return (
                    <div className="container mt-4">
                        <h2 className="text-primary mb-4">Interaktywne wykresy spółek</h2>
                        <ChartsView 
                            initialSymbols={[
                                // Technologia
                                "AAPL", "TSLA", "GOOGL", "AMZN", "MSFT", 
                                "META", "NVDA", "NFLX", "IBM", "ORCL",
                                
                                // Finanse
                                "JPM", "V", "MA", "BAC", "WFC",
                                "GS", "MS", "AXP", "BLK", "C",
                                
                                // Handel detaliczny
                                "WMT", "TGT", "COST", "HD", "LOW",
                                "SBUX", "MCD", "YUM", "NKE", "LULU",
                                
                                // Opieka zdrowotna
                                "JNJ", "PFE", "UNH", "ABBV", "MRK",
                                "BMY", "LLY", "AMGN", "CVS", "WBA",
                                
                                // Przemysł
                                "BA", "CAT", "GE", "MMM", "HON",
                                "UPS", "FDX", "RTX", "LMT", "NOC",
                                
                                // Telekomunikacja i Media
                                "T", "VZ", "CMCSA", "DIS", "NFLX",
                                
                                // Energia
                                "XOM", "CVX", "COP", "EOG", "SLB",
                                
                                // Technologia - dodatkowe
                                "INTC", "AMD", "PYPL", "CSCO", "ADBE",
                                "CRM", "QCOM", "TXN", "AMAT", "MU",
                                
                                // Dobra konsumpcyjne
                                "KO", "PEP", "PG", "MDLZ", "EL",
                                "CL", "KMB", "KHC", "HSY", "K"
                            ]} 
                            showMultiple={true} 
                        />
                    </div>
                );
            case "currency":
                return <CurrencyCalculator />;
            case "search":
                return <TickerSearch />;
            case "news":
                return <NewsPage />;
            default:
                return <HomePage onNavigate={setView} />;
        }
    };

    return (
        <div className="bg-white min-vh-100 text-dark">
            <Navbar bg="primary" variant="dark" expand="lg">
                <Container>
                    <Navbar.Brand
                        className="text-white"
                        role="button"
                        onClick={() => setView("home")}
                    >
                        <img
                            alt="logoStonks"
                            src="/logoStonks.png"
                            width="40"
                            height="45"
                            className="d-inline-block align-top"
                        />{' '}
                        StonksApp
                    </Navbar.Brand>
                    <Navbar.Toggle aria-controls="basic-navbar-nav" />
                    <Navbar.Collapse id="basic-navbar-nav">
                        <Nav className="ms-auto">
                            <Nav.Link onClick={() => setView("search")}>Wyszukaj Spółki</Nav.Link>
                            <Nav.Link onClick={() => setView("chartsview")}>Wykresy</Nav.Link>
                            <Nav.Link onClick={() => setView("table")}>Tabela</Nav.Link>
                            <Nav.Link onClick={() => setView("news")}>Newsy</Nav.Link>
                            <Nav.Link onClick={() => setView("currency")}>Kalkulator Walut</Nav.Link>
                        </Nav>
                    </Navbar.Collapse>
                </Container>
            </Navbar>
            {renderView()}
        </div>
    );
}

function HomePage({ onNavigate }) {
    return (
        <div className="d-flex flex-column justify-content-center align-items-center vh-100 text-center p-3 bg-light">
            <h1 className="display-5 fw-bold text-primary mb-3">Witaj w StonksApp</h1>
            <p className="text-secondary mb-4">Monitoruj rynek akcji w prosty i przejrzysty sposób</p>
            <div className="d-flex gap-3">
                <Button
                    variant="primary"
                    size="lg"
                    onClick={() => onNavigate("search")}
                >
                    Wyszukaj Spółki
                </Button>
                <Button
                    variant="outline-primary"
                    size="lg"
                    onClick={() => onNavigate("chartsview")}
                >
                    Wykresy
                </Button>
                <Button
                    variant="outline-primary"
                    size="lg"
                    onClick={() => onNavigate("table")}
                >
                    Tabela
                </Button>
                <Button
                    variant="outline-primary"
                    size="lg"
                    onClick={() => onNavigate("news")}
                >
                    Newsy
                </Button>
                <Button
                    variant="outline-primary"
                    size="lg"
                    onClick={() => onNavigate("currency")}
                >
                    Kalkulator Walut
                </Button>
            </div>
        </div>
    );
}

function StockTable({ stockData }) {
    return (
        <div className="container mt-4">
            <h2 className="text-primary">Notowania</h2>
            <table className="table table-bordered table-hover mt-3">
                <thead className="table-primary">
                    <tr>
                        <th>Symbol</th>
                        <th>Cena</th>
                        <th>Min</th>
                        <th>Max</th>
                    </tr>
                </thead>
                <tbody>
                    {stockData.map((stock, index) => (
                        <tr key={index}>
                            <td>{stock.symbol}</td>
                            <td>{stock.price}</td>
                            <td>{stock.min}</td>
                            <td>{stock.max}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default App; 