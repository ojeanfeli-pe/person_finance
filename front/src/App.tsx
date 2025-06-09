import { BrowserRouter, Routes, Route } from "react-router-dom";
import Header from "./components/Header";
import ListarTransacoes from "./pages/transacoes/ListarTransacoes";
import CadastrarTransacao from "./pages/transacoes/CadastrarTransacao";

function App() {

  return (
    <div>
      <BrowserRouter>
        <Header />
        <Routes>
          <Route path="/" element={<ListarTransacoes />} />
          <Route path="/pages/transacoes/listar" element={<ListarTransacoes />} />
          <Route path="/pages/transacoes/cadastrar" element={<CadastrarTransacao />} />
          <Route path="/cadastrar-transacao/:id" element={<CadastrarTransacao />} />
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;