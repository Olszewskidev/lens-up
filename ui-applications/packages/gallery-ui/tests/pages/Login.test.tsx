import { fireEvent, getByText, render, screen } from '@testing-library/react'
import { expect, test } from 'vitest';
import { LoginPage } from '../../src/pages/Login/LoginPage';
import { Provider } from 'react-redux'
import { loginInput, loginSubmit } from './Login.tsx';
import { BrowserRouter, RouterProvider } from 'react-router-dom';
import AppRouter from '../../src/pages/AppRouter.tsx';
import { JSX } from 'react/jsx-runtime';
import { store } from '../../src/app/store/store.ts';

/**
* @vitest-environment jsdom
*/
test('Entered code must change in the form', async () => {
    const {getByPlaceholderText, asFragment} = render(
      <Provider store={store}>
        <BrowserRouter>
          <LoginPage />
        </BrowserRouter>
      </Provider>
    );

    const firstRender = asFragment();

    fireEvent.change(getByPlaceholderText("Enter code"), {target: {value: "0"}});

    expect(asFragment()).toMatchObject(firstRender);
  });

/**
* @vitest-environment jsdom
*/
test('Form must be handled successfully', () => {
    const {getByText, asFragment} = render(
      <Provider store={store}>
        <BrowserRouter>
          <LoginPage />
        </BrowserRouter>
      </Provider>
    );

    const firstRender = asFragment();

    fireEvent.submit(getByText("Join"));
    
    expect(firstRender).toMatchSnapshot(asFragment());
  });

/**
* @vitest-environment jsdom
*/
test('Login must set gallery successfuly', async () => {
  const {asFragment, baseElement} = await loginInput();

  const firstRender = asFragment();

  fireEvent.submit(getByText(baseElement, "Join"));
  
  expect(asFragment()).toMatchSnapshot();
});

function renderWithRouter(arg0: JSX.Element, arg1: { route: string; }) {
  throw new Error('Function not implemented.');
}
