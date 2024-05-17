import renderer from 'react-test-renderer';
import { RouterProvider } from 'react-router-dom'
import { store } from '../src/app/store/store'
import { AppRouter } from '../src/App'
import App from '../src/App';

it('App must render', () => {
    const component = renderer.create(
        <App />,
    );
    let tree = component.toJSON();
    expect(tree).toMatchSnapshot();
  });