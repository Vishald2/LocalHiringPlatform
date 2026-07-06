import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App.tsx'
import "./styles/global.css";
import "./styles/layout.css";
import "./styles/forms.css";
import "./styles/navigation.css";
import "./styles/cards.css";
import "./styles/tables.css";
import "./styles/dashboard.css";
import "./styles/utilities.css";
import "./styles/aichat.css";

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <App />
  </StrictMode>,
)
