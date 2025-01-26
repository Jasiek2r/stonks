import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";

import Navbar from "react-bootstrap/Navbar";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";

const StockData = [
  {
    symbol: "AAPL",
    price: 150,
    min: 100,
    max: 200,
  },
  {
    symbol: "GOOGL",
    price: 2000,
    min: 1500,
    max: 2500,
  },
  {
    symbol: "TSLA",
    price: 700,
    min: 500,
    max: 800,
  },
  {
    symbol: "AMZN",
    price: 3000,
    min: 2500,
    max: 3500,
  },
];

function App() {
  return (
    <>
      <NavBar />
      <Main stockData={StockData} />
    </>
  );
}

function NavBar() {
  return (
    <Navbar className="bg-dark">
      <Navbar.Brand className="text-white ps-2">
        <img
          alt="logoStonks"
          src="/logoStonks.png"
          width="50"
          height="55"
          className="d-inline-block align-top"
        />{" "}
        StonksApp
      </Navbar.Brand>
      <Form inline className="d-flex ms-auto">
        <Form.Group className="me-3" controlId="formBasicEmail">
          <Form.Label className="text-white">Login:</Form.Label>
          <Form.Control
            type="email"
            placeholder="wpisz e-mail"
            className="form-control"
          />
        </Form.Group>
        <Form.Group className="me-3" controlId="formBasicPassword">
          <Form.Label className="text-white">Hasło:</Form.Label>
          <Form.Control
            type="password"
            placeholder="wpisz hasło"
            className="form-control"
          />
        </Form.Group>
        <Button variant="primary" type="submit" className="ml-2">
          Zaloguj się
        </Button>
      </Form>
    </Navbar>
  );
}

function Main({ stockData }) {
  return (
    <div className="container">
      <table className="table mt-5">
        <thead>
          <tr>
            <th>Symbol</th>
            <th>Cena</th>
            <th>Min</th>
            <th>Max</th>
          </tr>
        </thead>
        <tbody>
          {stockData.map((stock, index) => (
            <Stock
              key={index}
              symbol={stock.symbol}
              price={stock.price}
              min={stock.min}
              max={stock.max}
            />
          ))}
        </tbody>
      </table>
    </div>
  );
}

function Stock({ symbol, price, min, max }) {
  return (
    <tr>
      <td>{symbol}</td>
      <td>{price}</td>
      <td>{min}</td>
      <td>{max}</td>
    </tr>
  );
}

export default App;
