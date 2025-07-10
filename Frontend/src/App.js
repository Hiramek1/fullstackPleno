import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import CompanyRegistration from './pages/CompanyRegistration';
import InvoiceManagement from './pages/InvoiceManagement';
import AnticipationCart from './pages/AnticipationCart';
import Navbar from './components/Navbar';
import './App.css';

function App() {
  return (
    <Router>
      <Navbar />
      <div className="container mx-auto px-4">
        <Routes>
          <Route path="/" element={<CompanyRegistration />} />
          <Route path="/invoices" element={<InvoiceManagement />} />
          <Route path="/cart" element={<AnticipationCart />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
