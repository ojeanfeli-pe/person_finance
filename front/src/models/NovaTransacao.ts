import { Categoria } from "./Categoria";

export interface NovaTransacao {
  description: string;
  amount: number;
  type: string;
  categoryId: number;
}
