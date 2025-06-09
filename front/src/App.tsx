import { BrowserRouter, Routes, Route } from "react-router-dom";
import Header from "./components/Header";
import ListarTransacoes from "./pages/transacoes/ListarTransacoes";
import CadastrarTransacao from "./pages/transacoes/CadastrarTransacao";
import Home from "./pages/Home"; 
import './styles/App.css'

function App() {
  return (
    <div>
      <BrowserRouter>
        <Header />
        <Routes>
          <Route path="/" element={<Home />} /> 
          <Route path="/pages/transacoes/listar" element={<ListarTransacoes />} />
          <Route path="/pages/transacoes/cadastrar" element={<CadastrarTransacao />} />
          <Route path="/atualizar-transacao/:id" element={<CadastrarTransacao />} />
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
