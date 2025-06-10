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
  const [tipo, setTipo] = useState("Entrada");
  const [categoriaId, setCategoriaId] = useState(0);
  const [categorias, setCategorias] = useState<Categoria[]>([]);
  const navigate = useNavigate();
  const { id } = useParams();

  // Carrega todas as categorias da API
  useEffect(() => {
    axios.get("http://localhost:5000/api/categories")
      .then((response) => setCategorias(response.data))
      .catch(() => alert("Erro ao carregar categorias"));
  }, []);

  // Se estiver editando, carrega a transação existente
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

  // Lógica de envio do formulário
  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    if (descricao.trim().length < 3) {
      alert("A descrição deve conter pelo menos 3 caracteres.");
      return;
    }

    if (valor < 0) {
      alert("O valor não pode ser negativo.");
      return;
    }

    if (categoriaId === 0) {
      alert("Por favor, selecione uma categoria.");
      return;
    }

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
          navigate("/pages/transacoes/listar");
        })
        .catch(() => alert("Erro ao atualizar transação"));
    } else {
      axios.post("http://localhost:5000/api/transactions", novaTransacao)
        .then(() => {
          alert("Transação cadastrada com sucesso!");
          navigate("/pages/transacoes/listar");
        })
        .catch(() => alert("Erro ao cadastrar transação"));
    }
  };

  // Filtra categorias com base no tipo da transação
  const categoriasFiltradas = categorias.filter(cat =>
    tipo.toLowerCase() === "entrada"
      ? cat.type === "entrada"
      : cat.type === "saida"
  );

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
            min={0}
            step="0.01"
            required
          />
        </div>
        <div>
          <label>Tipo:</label>
          <select value={tipo} onChange={(e) => setTipo(e.target.value)} required>
            <option value="Entrada">Entrada</option>
            <option value="Saída">Saída</option>
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
            {categoriasFiltradas.map((cat) => (
              <option key={cat.id} value={cat.id}>
                {cat.name}
              </option>
            ))}
          </select>
        </div>
        <button type="submit">{id ? "Atualizar" : "Salvar"}</button>
      </form>
    </div>
  );
}

export default CadastrarTransacao;
