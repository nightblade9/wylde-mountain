import React from 'react';
import ReactDOM from 'react-dom';
import { MemoryRouter } from 'react-router-dom';
import { RequireAuthentication } from './RequireAuthentication';

it('redirects to login if not authenticated', async () => {
  const div = document.createElement('div');
  ReactDOM.render(
    // localStorage.getItem("userInfo") is null
    <MemoryRouter>
      <RequireAuthentication />
    </MemoryRouter>, div);
  await new Promise(resolve => setTimeout(resolve, 1000));
  expect(div.hasAttribute("Redirect"));
});