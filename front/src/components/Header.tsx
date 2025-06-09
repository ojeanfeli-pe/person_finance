import { Link } from "react-router-dom";
import "./Header.css";
import { FaWallet } from "react-icons/fa";

function Header() {
  return (
    <header className="header">
      <nav className="nav">
        <h2 className="titulo">
        {FaWallet({ style: { marginRight: '20px'}, size: 28 })}
          Finanças Pessoais</h2>
        <ul className="nav-lista">
          <li>
            <Link to="/" className="nav-link">
              Home
            </Link>
          </li>
          <li>
            <Link to="/pages/transacoes/listar" className="nav-link">
              Lista de Transações
            </Link>
          </li>
          <li>
            <Link to="/pages/transacoes/cadastrar" className="nav-link">
              Cadastrar Transação
            </Link>
          </li>
        </ul>
      </nav>
    </header>
  );
}

export default Header;
