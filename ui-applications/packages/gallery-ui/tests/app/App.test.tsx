import { render, screen } from '@testing-library/react'
import { vi, expect, test } from 'vitest'
import App from '../../src/App';
import { createAsyncThunk } from '@reduxjs/toolkit';
import { create } from './store';

/**
* @vitest-environment jsdom
*/
test('Middleware must log api failure', async () => {
    const { next, invoke } = create();
    const consoleMock = vi.spyOn(console, 'error').mockImplementation(() => undefined);
    const action = createAsyncThunk("test/accepted", async (state: string, { rejectWithValue }) => {
        if (state != "accepted")
          return rejectWithValue("Console must read api failure");
      });
    invoke(action);
    expect(consoleMock).toHaveBeenCalledOnce();
    expect(consoleMock).toHaveBeenLastCalledWith('API Failed!');
  });

/**
* @vitest-environment jsdom
*/
test('App must render according to snapshot', () => {
    render(
        <App />,
    );

    expect(screen).toMatchSnapshot();
  });