import { SetStateAction, useEffect, useState } from "react";
import { Transacao } from "../../models/Transacao";
import axios from "axios";
import { Link } from "react-router-dom";
import '../../styles/transacao.css';
import { FaEdit, FaTrash } from 'react-icons/fa';

function ListarTransacoes() {
    const [transacoes, setTransacoes] = useState<Transacao[]>([]);

    useEffect(() => {
        carregarTransacoes();
    }, []);

    function carregarTransacoes() {
        axios.get("http://localhost:5000/api/transactions")
            .then((response: { data: SetStateAction<Transacao[]>; }) => {
                setTransacoes(response.data);
                console.table(response.data);
            })
            .catch(() => {
                alert("Erro ao carregar transações");
            });
    }

    function remover(id: string) {
        axios.delete(`http://localhost:5000/api/transactions/${id}`)
            .then(() => {
                alert("Produto removido com sucesso");
                setTransacoes((prev) => prev.filter(t => t.id !== id));
            })
            .catch(() =>
                alert("Não foi possível remover o produto")
            );
    }

    // Cálculos
    const totalEntrada = transacoes
        .filter(t => t.type.toLowerCase() === "entrada")
        .reduce((soma, t) => soma + Number(t.amount), 0);

    const totalSaida = transacoes
        .filter(t => t.type.toLowerCase() === "saída")
        .reduce((soma, t) => soma + Number(t.amount), 0);

    const saldoFinal = totalEntrada - totalSaida;

    return (
        <div className="container">

            {/* Barra de totais */}
            <div className="resumo-financeiro">
                <div className="resumo-box resumo-entrada">
                    Entrada: {new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(totalEntrada)}
                </div>
                <div className="resumo-box resumo-total">
                    Total de Gastos: {new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(saldoFinal)}
                </div>
                <div className="resumo-box resumo-saida">
                    Saída: {new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(totalSaida)}
                </div>
            </div>

            <h1>Lista de Produtos</h1>

            <table>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Descrição</th>
                        <th>Valor</th>
                        <th>Data</th>
                        <th>Tipo</th>
                        <th>Categoria</th>
                        <th>Ações</th>
                    </tr>
                </thead>
                <tbody>
                    {transacoes.length === 0 ? (
                        <tr>
                            <td colSpan={7} style={{ textAlign: "center", padding: "20px" }}>
                                Nenhuma transação encontrada
                            </td>
                        </tr>
                    ) : (
                        transacoes.map((t) => (
                            <tr key={t.id}>
                                <td>{t.id}</td>
                                <td>{t.description}</td>
                                <td>
                                    {new Intl.NumberFormat('pt-BR', {
                                        style: 'currency',
                                        currency: 'BRL'
                                    }).format(Number(t.amount))}
                                </td>
                                <td>{new Date(t.date).toLocaleDateString('pt-BR')}</td>
                                <td>{t.type}</td>
                                <td>{t.category.name}</td>
                                <td>
                                    <button className="remover" onClick={() => remover(t.id)}>
                                        {FaTrash({ style: { marginRight: '5px' } })}
                                    </button>
                                    <Link to={`/atualizar-transacao/${t.id}`}>
                                        <button className="editar">
                                            {FaEdit({ style: { marginRight: '5px' } })}
                                        </button>
                                    </Link>
                                </td>
                            </tr>
                        ))
                    )}
                </tbody>
            </table>
        </div>
    );
}

export default ListarTransacoes;