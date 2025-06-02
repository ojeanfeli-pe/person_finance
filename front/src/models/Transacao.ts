import { Categoria } from "./Categoria";

export interface Transacao {
  id: number;
  description: string;
  amount: number;
  date: string; // ISO string
  type: string;
  category: Categoria;
  categoryId: number;
}
