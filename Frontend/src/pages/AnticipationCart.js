import React, { useState, useEffect } from 'react';
import axios from 'axios';
import API_BASE_URL from '../api/apiConfig';

const AnticipationCart = () => {
  const [cart, setCart] = useState([]);
  const [company, setCompany] = useState(null);
  const [calculationResult, setCalculationResult] = useState(null);
  const [calculationError, setCalculationError] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    setLoading(true);
    axios.get(`${API_BASE_URL}/api/Cart`, { params: { companyCnpj: company?.cnpj } })
      .then(response => {
        setCart(response.data.items);
        return axios.get(`${API_BASE_URL}/api/Company/${company?.cnpj}`);
      })
      .then(response => {
        setCompany(response.data);
        setLoading(false);
      })
      .catch(error => {
        console.error('Erro ao buscar dados:', error);
        setError('Erro ao carregar dados. Tente novamente.');
        setLoading(false);
      });
  }, []);

  const handleRemoveFromCart = (invoiceId) => {
    axios.delete(`${API_BASE_URL}/api/Cart/${invoiceId}`)
      .then(() => {
        setCart(cart.filter(item => item.id !== invoiceId));
      })
      .catch(error => console.error('Erro ao remover nota do carrinho:', error));
  };

  const handleCalculate = () => {
    setCalculationError(null);
    setCalculationResult(null);
    
    axios.post(`${API_BASE_URL}/api/Advance/Calculate`, {
      CompanyCnpj: company.cnpj,
      InvoiceNumbers: cart.map(item => item.number)
    })
      .then(response => {
        setCalculationResult(response.data);
      })
      .catch(error => {
        if (error.response) {
          // Backend returned an error response (4xx/5xx)
          const errorMessage = error.response.data || 'Erro desconhecido no servidor';
          setCalculationError(errorMessage);
        } else {
          // Network or other errors
          setCalculationError('Erro de conexão com o servidor');
        }
        console.error('Erro ao calcular antecipação:', error);
      });
  };

  if (loading) {
    return <div className="text-center py-8">Carregando...</div>;
  }

  if (error) {
    return <div className="text-center py-8 text-red-500">{error}</div>;
  }

  return (
    <div className="max-w-4xl mx-auto mt-10">
      <h1 className="text-2xl font-bold mb-6">Carrinho de Antecipação</h1>
      
      {company && (
        <div className="mb-6 p-4 bg-gray-50 rounded-lg">
          <h2 className="text-lg font-semibold">Empresa: {company.name}</h2>
          <p>CNPJ: {company.cnpj}</p>
          <p>Limite de Antecipação: R$ {company.limit.toFixed(2)}</p>
        </div>
      )}

      <div className="mb-8">
        <h2 className="text-xl font-semibold mb-4">Notas Fiscais no Carrinho</h2>
        {cart.length === 0 ? (
          <p>Nenhuma nota fiscal no carrinho.</p>
        ) : (
          <div className="bg-white shadow-md rounded-lg overflow-hidden">
            <table className="min-w-full divide-y divide-gray-200">
              <thead className="bg-gray-50">
                <tr>
                  <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Número</th>
                  <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Valor (R$)</th>
                  <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Vencimento</th>
                  <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Ações</th>
                </tr>
              </thead>
              <tbody className="bg-white divide-y divide-gray-200">
                {cart.map((item) => (
                  <tr key={item.id}>
                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">{item.number}</td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">{item.value}</td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">{item.dueDate}</td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm font-medium">
                      <button 
                        onClick={() => handleRemoveFromCart(item.id)}
                        className="text-red-600 hover:text-red-900"
                      >
                        Remover
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </div>

      <div className="text-center">
        <button
          onClick={handleCalculate}
          disabled={cart.length === 0}
          className={`py-2 px-4 rounded-md shadow-sm text-sm font-medium text-white ${cart.length === 0 ? 'bg-gray-400' : 'bg-blue-600 hover:bg-blue-700'} focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500`}
        >
          Calcular Antecipação
        </button>
      </div>

      {calculationError && (
        <div className="mt-4 p-4 bg-red-50 text-red-700 rounded-lg">
          <strong>Erro:</strong> {calculationError}
        </div>
      )}

      {calculationResult && (
        <div className="mt-8 p-6 bg-gray-50 rounded-lg shadow-md">
          <h2 className="text-xl font-semibold mb-4">Resultado da Antecipação</h2>
          <div className="mb-4">
            <p><strong>Empresa:</strong> {calculationResult.empresa}</p>
            <p><strong>CNPJ:</strong> {calculationResult.cnpj}</p>
            <p><strong>Limite:</strong> R$ {calculationResult.limite.toFixed(2)}</p>
          </div>
          <div>
            <h3 className="text-lg font-medium mb-2">Notas Fiscais:</h3>
            <ul className="space-y-2">
              {calculationResult.notas_fiscais.map((nota, index) => (
                <li key={index} className="p-3 bg-white rounded shadow-md">
                  <p><strong>Número:</strong> {nota.numero}</p>
                  <p><strong>Valor Bruto:</strong> R$ {nota.valor_bruto.toFixed(2)}</p>
                  <p><strong>Valor Líquido:</strong> R$ {nota.valor_liquido.toFixed(2)}</p>
                </li>
              ))}
            </ul>
          </div>
          <div className="mt-4 pt-4 border-t border-gray-200">
            <p><strong>Total Bruto:</strong> R$ {calculationResult.total_bruto.toFixed(2)}</p>
            <p><strong>Total Líquido:</strong> R$ {calculationResult.total_liquido.toFixed(2)}</p>
          </div>
        </div>
      )}
    </div>
  );
};

export default AnticipationCart;
