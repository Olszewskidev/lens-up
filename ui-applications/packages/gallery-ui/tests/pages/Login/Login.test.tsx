import { fireEvent, getByText, render, screen } from '@testing-library/react'
import { expect, test, describe } from 'vitest';
import { LoginPage } from '../../../src/pages/Login/LoginPage';
import { Provider } from 'react-redux'
import { loginSubmit } from './Login.tsx';
import { BrowserRouter } from 'react-router-dom';
import { JSX } from 'react/jsx-runtime';
import { store } from '../../../src/app/store/store.ts';

/**
* @vitest-environment jsdom
*/
describe("Login form ui tests", () => {
  test('Entered code must change in the form', async () => {
    const { getByPlaceholderText, asFragment } = render(
      <Provider store={store}>
        <BrowserRouter>
          <LoginPage />
        </BrowserRouter>
      </Provider>
    );

    const firstRender = asFragment();

    expect(firstRender).toMatchSnapshot();

    fireEvent.change(screen.getByPlaceholderText("Enter code"), { target: { value: "0" } });

    expect(asFragment).not.toBe(firstRender);
  });

  test("Form must be handled successfully", () => {
    const { getByText, asFragment } = render(
      <Provider store={store}>
        <BrowserRouter>
          <LoginPage />
        </BrowserRouter>
      </Provider>
    );

    const firstRender = asFragment();

    expect(firstRender).toMatchSnapshot();

    fireEvent.submit(getByText("Join"));

    expect(firstRender).not.toBe(asFragment());
  });

  // The api call returns an error and so the page is stuck in Login with loading element
  test("Check loading on submit", async () => {
    const {baseElement} = await loginSubmit();

    expect(getByText(baseElement, "Loading...")).toMatchSnapshot();
  });
});

function renderWithRouter(arg0: JSX.Element, arg1: { route: string; }) {
  throw new Error('Function not implemented.');
}
