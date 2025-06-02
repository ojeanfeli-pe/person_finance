import { useEffect, useState } from "react";
import { Transacao } from "../../models/Transacao";

function ListarTransacoes() {
  const [transacoes, setTransacoes] = useState<Transacao[]>([]);

  useEffect(() => {
    fetch("http://localhost:5000/api/transactions")
      .then(res => res.json())
      .then(data => setTransacoes(data))
      .catch(err => console.error(err));
  }, []);

  return (
    <div>
      <h1>Lista de Transações</h1>
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Descrição</th>
            <th>Valor</th>
            <th>Data</th>
            <th>Tipo</th>
            <th>Categoria</th>
          </tr>
        </thead>
        <tbody>
          {transacoes.map(t => (
            <tr key={t.id}>
              <td>{t.id}</td>
              <td>{t.description}</td>
              <td>{t.amount.toFixed(2)}</td>
              <td>{new Date(t.date).toLocaleDateString()}</td>
              <td>{t.type}</td>
              <td>{t.category?.name}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default ListarTransacoes;
