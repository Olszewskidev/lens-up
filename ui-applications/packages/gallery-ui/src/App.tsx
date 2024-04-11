import { Provider } from 'react-redux'
import './App.css'
import { RouterProvider } from 'react-router-dom'
import { store } from './app/store'
import { AppRouter } from './pages'

function App() {
  return (
    <Provider store={store}>
      <RouterProvider router={AppRouter} />
    </Provider>
  )
}

export default App
