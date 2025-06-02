import { Link } from "react-router-dom";

function Header() {

    return (
        <header>
            <nav className="navbar">
                <div className="logo">Transações</div>
                    <ul className="nav-links">
                        <li>
                            <Link to="/pages/transacoes/ListarTransacoes">
                                Lista de trasações
                            </Link>
                        </li>
                        <li>
                            <Link to="/pages/transacoes/CadastrarTransacao">
                                Cadastrar trasações
                            </Link>
                        </li>
                    </ul>
                </nav>
        </header>
    )
}


export default Header;
