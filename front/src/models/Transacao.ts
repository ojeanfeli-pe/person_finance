import { Categoria } from "./Categoria";

export interface Transacao {
  id: string;
  description: string;
  amount: number;
  date: Date;
  type: string;
  category: Categoria;
  categoryId: number;
}
