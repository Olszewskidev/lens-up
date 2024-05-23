import { render } from '@testing-library/react'
import App from '../src/App'

export const AppMock = () => {
    return render(<App />);
}