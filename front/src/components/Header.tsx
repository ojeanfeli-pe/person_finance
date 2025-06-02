import { Link } from "react-router-dom";

function Header() {
  return (
    <header>
      <nav>
        <Link to="/pages/transacoes/listar">Listar Transações</Link> |{" "}
        <Link to="/pages/transacoes/cadastrar">Cadastrar Transação</Link>
      </nav>
    </header>
  );
}

export default Header;
