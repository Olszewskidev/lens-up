import {fireEvent, render, screen} from '@testing-library/react'
import {expect, it} from '@jest/globals';
import { LoginPage } from '../../src/pages/Login/LoginPage';
import { createAsyncThunk } from '@reduxjs/toolkit';
import { create } from './app/store.test';

it('Entered code must change in the form', () => {
    const {getByText, asFragment} = render(
        <LoginPage />,
    );

    const firstRender = asFragment();

    fireEvent.change(getByText("Join to your gallery"));
    
    expect(firstRender).toMatchSnapshot(asFragment());
  });