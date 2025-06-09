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
        .then( (response: { data: SetStateAction<Transacao[]>; }) =>{
            setTransacoes(response.data);
            console.table(response.data);
        })
        .catch( () => {
            alert("error");
        });
    }

    function remover(id: string) {
        axios.delete(`http://localhost:5000/api/transactions/${id}`)
        .then( () => {
            alert("Produto removido com sucesso");
            carregarTransacoes();
        })
        .catch( () => 
            alert("Não foi possivel remover o produto")
        )
    }

    return (
        <div className="container">
            <h1>
                Lista de Produtos
            </h1>

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
                    {transacoes.map((t) => (
                        <tr key={t.id}>
                            <td>{t.id}</td>
                            <td>{t.description}</td>
                            <td>{t.amount}</td>
                            <td>{new Date(t.date).toLocaleDateString('pt-BR')}</td>
                            <td>{t.type}</td>
                            <td>{t.category.name}</td>
                            <td>
                                <button className="remover"
                                        onClick={() => remover(t.id)}>
                                {FaTrash({ style: { marginRight: '5px' } })}
                                </button>

                                <Link to={`/atualizar-transacao/${t.id}`}>
                                    <button className="editar">
                                    {FaEdit({ style: { marginRight: '5px' } })}
                                    </button>
                                </Link>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    )

};

export default ListarTransacoes;

