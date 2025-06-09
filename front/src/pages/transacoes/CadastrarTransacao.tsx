import { useEffect, useState } from "react";
import axios from "axios";
import { NovaTransacao } from "../../models/NovaTransacao";
import { useNavigate, useParams } from "react-router-dom";
import { Categoria } from "../../models/Categoria";
import { Transacao } from "../../models/Transacao";
import '../../styles/transacao.css';

function CadastrarTransacao() {
  const [descricao, setDescricao] = useState("");
  const [valor, setValor] = useState(0);
  const [tipo, setTipo] = useState("income");
  const [categoriaId, setCategoriaId] = useState(0);
  const [categorias, setCategorias] = useState<Categoria[]>([]);
  const navigate = useNavigate();
  const { id } = useParams(); 

  
  useEffect(() => {
    axios.get("http://localhost:5000/api/categories")
      .then((response) => setCategorias(response.data))
      .catch(() => alert("Erro ao carregar categorias"));
  }, []);

 
  useEffect(() => {
    if (id) {
      axios.get(`http://localhost:5000/api/transactions/${id}`)
        .then((response) => {
          const transacao: Transacao = response.data;
          setDescricao(transacao.description);
          setValor(transacao.amount);
          setTipo(transacao.type);
          setCategoriaId(transacao.categoryId);
        })
        .catch(() => alert("Erro ao carregar transação"));
    }
  }, [id]);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    const novaTransacao: NovaTransacao = {
      description: descricao,
      amount: valor,
      type: tipo,
      categoryId: categoriaId,
    };

    if (id) {
      axios.put(`http://localhost:5000/api/transactions/${id}`, novaTransacao)
        .then(() => {
          alert("Transação atualizada com sucesso!");
          navigate("/transacoes/listar");
        })
        .catch(() => alert("Erro ao atualizar transação"));
    } else {
      
      axios.post("http://localhost:5000/api/transactions", novaTransacao)
        .then(() => {
          alert("Transação cadastrada com sucesso!");
          navigate("/transacoes/listar");
        })
        .catch(() => alert("Erro ao cadastrar transação"));
    }
  };

  return (
    <div className="container">
      <h1>{id ? "Editar Transação" : "Cadastrar Nova Transação"}</h1>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Descrição:</label>
          <input
            type="text"
            value={descricao}
            onChange={(e) => setDescricao(e.target.value)}
            required
          />
        </div>
        <div>
          <label>Valor:</label>
          <input
            type="number"
            value={valor}
            onChange={(e) => setValor(parseFloat(e.target.value))}
            required
          />
        </div>
        <div>
          <label>Tipo:</label>
          <select value={tipo} onChange={(e) => setTipo(e.target.value)} required>
            <option value="Entrada">Entrada</option>
            <option value="Saida">Saída</option>
          </select>
        </div>
        <div>
          <label>Categoria:</label>
          <select
            value={categoriaId}
            onChange={(e) => setCategoriaId(parseInt(e.target.value))}
            required
          >
            <option value={0}>Selecione</option>
            {categorias.map((cat) => (
              <option key={cat.id} value={cat.id}>{cat.name}</option>
            ))}
          </select>
        </div>
        <button type="submit">{id ? "Atualizar" : "Salvar"}</button>
      </form>
    </div>
  );
}

export default CadastrarTransacao;
