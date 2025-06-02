import { BrowserRouter, Routes, Route } from "react-router-dom";
import Header from "./components/Header";
import ListarTransacoes from "./pages/transacoes/ListarTransacoes";

function App() {

  return (
    <div>
      <BrowserRouter>
        <Header />
        <Routes>
          <Route path="/" element={<ListarTransacoes />} />
          <Route path="/pages/transacoes/listar" element={<ListarTransacoes />} />
        </Routes>
      </BrowserRouter>
    </div>
  );
}
//4 - OBRIGATORIAMENTE o componente DEVE ser exportado
export default App;