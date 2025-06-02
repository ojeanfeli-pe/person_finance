import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Categoria } from "../../models/Categoria";
import { Transacao } from "../../models/Transacao";

function CadastrarTransacao(){
      const { id } = useParams();
  const [categorias, setCategorias] = useState<Categoria[]>([]);
  const [transacao, setTransacao] = useState<Partial<Transacao>>({
    description: "",
    amount: 0,
    date: new Date().toISOString().split("T")[0],
    type: "Entrada",
    categoryId: 0,
  });
}

export default CadastrarTransacao;