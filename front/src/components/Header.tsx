import { Link } from "react-router-dom";

function Header() {

    return (
        <header>
            <nav>
                <div >Transações</div>
                        <li>
                            <Link to="/pages/transacoes/listar">
                                Lista de trasações
                            </Link>
                        </li>
                        <li>
                            <Link to="/pages/transacoes/CadastrarTransacao">
                                Cadastrar trasações
                            </Link>
                        </li>
                </nav>
        </header>
    )
}


export default Header;
