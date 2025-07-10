import React from 'react';
import { Link } from 'react-router-dom';

export default function Navbar() {
  return (
    <nav className="bg-blue-600 p-4 text-white">
      <div className="container mx-auto flex justify-between items-center">
        <Link to="/" className="text-xl font-bold">Antecipação de Recebíveis</Link>
        <div className="space-x-4">
          <Link to="/" className="hover:underline">Cadastro Empresa</Link>
          <Link to="/invoices" className="hover:underline">Notas Fiscais</Link>
          <Link to="/cart" className="hover:underline">Carrinho</Link>
        </div>
      </div>
    </nav>
  );
}
