import { Link } from "react-router-dom";
import "../styles/home.css";

export default function Home() {
  return (
    <div className="home-container">
      <h1>Seja bem-vindo à sua página de Finanças Pessoais</h1>
      <p>Organize seus gastos, visualize suas transações e mantenha o controle!</p>
      <Link to="/pages/transacoes/cadastrar">
        <button className="home-button">Abrir sua agenda</button>
      </Link>
    </div>
  );
}
