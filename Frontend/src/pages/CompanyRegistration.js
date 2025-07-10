import React, { useState } from 'react';
import axios from 'axios';
import API_BASE_URL from '../api/apiConfig';

const CompanyRegistration = () => {
  const [formData, setFormData] = useState({
    cnpj: '',
    name: '',
    monthlyRevenue: '',
    sector: '',
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await axios.post(`${API_BASE_URL}/companies`, formData);
      alert('Empresa cadastrada com sucesso!');
    } catch (error) {
      alert('Erro ao cadastrar empresa: ' + error.message);
    }
  };

  return (
    <div className="max-w-2xl mx-auto mt-10 p-6 bg-white rounded-lg shadow-md">
      <h1 className="text-2xl font-bold mb-6">Cadastro de Empresa</h1>
      <form onSubmit={handleSubmit} className="space-y-4">
        <div>
          <label className="block text-sm font-medium text-gray-700">CNPJ</label>
          <input
            type="text"
            name="cnpj"
            value={formData.cnpj}
            onChange={handleChange}
            required
            className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-blue-500 focus:border-blue-500"
          />
        </div>
        <div>
          <label className="block text-sm font-medium text-gray-700">Nome</label>
          <input
            type="text"
            name="name"
            value={formData.name}
            onChange={handleChange}
            required
            className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-blue-500 focus:border-blue-500"
          />
        </div>
        <div>
          <label className="block text-sm font-medium text-gray-700">Faturamento Mensal (R$)</label>
          <input
            type="number"
            name="monthlyRevenue"
            value={formData.monthlyRevenue}
            onChange={handleChange}
            required
            className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-blue-500 focus:border-blue-500"
          />
        </div>
        <div>
          <label className="block text-sm font-medium text-gray-700">Ramo</label>
          <select
            name="sector"
            value={formData.sector}
            onChange={handleChange}
            required
            className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-blue-500 focus:border-blue-500"
          >
            <option value="">Selecione</option>
            <option value="Serviços">Serviços</option>
            <option value="Produtos">Produtos</option>
          </select>
        </div>
        <button
          type="submit"
          className="w-full flex justify-center py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500"
        >
          Cadastrar
        </button>
      </form>
    </div>
  );
};

export default CompanyRegistration;
