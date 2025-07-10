import React, { useState, useEffect } from 'react';
import api from '../api/apiConfig';

const InvoiceManagement = () => {
  const [invoices, setInvoices] = useState([]);
  const [companyCnpjInput, setCompanyCnpjInput] = useState('');
  const [companyCnpj, setCompanyCnpj] = useState('');
  const [formData, setFormData] = useState({
    number: '',
    value: '',
    dueDate: ''
  });
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (companyCnpj) {
      setLoading(true);
      api.get(`/api/Cart/${companyCnpj}`)
        .then(response => {
          const invoices = response.data.items.map(item => ({
            number: item.invoiceNumber,
            amount: item.amount,
            dueDate: item.dueDate
          }));
          setInvoices(invoices);
          setLoading(false);
        })
        .catch(error => {
          console.error('Erro ao buscar notas fiscais:', error);
          setLoading(false);
        });
    }
  }, [companyCnpj]);

  const handleLoadInvoices = () => {
    setCompanyCnpj(companyCnpjInput);
  };

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
      await api.post(`/api/Registration/Company/${companyCnpj}/Invoice`, formData);
      alert('Nota fiscal cadastrada com sucesso!');

      setLoading(true);
      api.get(`/api/Cart/${companyCnpj}`)
        .then(response => {
          const invoices = response.data.items.map(item => ({
            number: item.invoiceNumber,
            amount: item.amount,
            dueDate: item.dueDate
          }));
          setInvoices(invoices);
          setLoading(false);
        })
        .catch(error => {
          console.error('Erro ao buscar notas fiscais:', error);
          setLoading(false);
        });

      setFormData({ number: '', value: '', dueDate: '' });
    } catch (error) {
      alert('Erro ao cadastrar nota fiscal: ' + error.message);
    }
  };

  const handleAddToCart = (invoiceNumber) => {
    api.post(`/api/Cart/${companyCnpj}/items`, { invoiceNumber })
      .then(() => {
        alert('Nota fiscal adicionada ao carrinho!');
      })
      .catch(error => {
        console.error('Erro ao adicionar ao carrinho:', error);
      });
  };

  return (
    <div className="max-w-4xl mx-auto mt-10">
      <h1 className="text-2xl font-bold mb-6">Cadastro de Notas Fiscais</h1>
      
      <div className="mb-4">
        <label htmlFor="companyCnpj" className="block text-sm font-medium text-gray-700">CNPJ da Empresa</label>
        <div className="mt-1 flex rounded-md shadow-sm">
          <input
            type="text"
            id="companyCnpj"
            className="focus:ring-blue-500 focus:border-blue-500 flex-1 block w-full rounded-md sm:text-sm border-gray-300"
            placeholder="Digite o CNPJ"
            value={companyCnpjInput}
            onChange={(e) => setCompanyCnpjInput(e.target.value)}
          />
          <button
            type="button"
            className="ml-3 inline-flex items-center px-4 py-2 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500"
            onClick={handleLoadInvoices}
          >
            Carregar Notas
          </button>
        </div>
      </div>

      <form onSubmit={handleSubmit} className="space-y-4 mb-8">
        <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
          <div>
            <label className="block text-sm font-medium text-gray-700">Número</label>
            <input
              type="text"
              name="number"
              value={formData.number}
              onChange={handleChange}
              required
              className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-blue-500 focus:border-blue-500"
            />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700">Valor (R$)</label>
            <input
              type="number"
              name="value"
              value={formData.value}
              onChange={handleChange}
              required
              className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-blue-500 focus:border-blue-500"
            />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700">Data de Vencimento</label>
            <input
              type="date"
              name="dueDate"
              value={formData.dueDate}
              onChange={handleChange}
              required
              className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-blue-500 focus:border-blue-500"
            />
          </div>
        </div>
        <button
          type="submit"
          className="w-full md:w-auto py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500"
        >
          Adicionar Nota Fiscal
        </button>
      </form>

      <h2 className="text-xl font-semibold mb-4">Notas Fiscais Cadastradas</h2>
      {loading ? (
        <p>Carregando...</p>
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
              {invoices.map((invoice) => (
                <tr key={invoice.number}>
                  <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">{invoice.number}</td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">{invoice.amount}</td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">{invoice.dueDate}</td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm font-medium">
                    <button 
                      className="text-blue-600 hover:text-blue-900"
                      onClick={() => handleAddToCart(invoice.number)}
                    >
                      Adicionar ao Carrinho
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
};

export default InvoiceManagement;
