import React from 'react';
import ReactDOM from 'react-dom';
import { MemoryRouter } from 'react-router-dom';
import { CoreGame } from './CoreGame';

beforeEach(() => {
  fetch.resetMocks();
});

it('CoreGame renders current floor when user is authenticated', async () => {
  
  fetch
    .once(JSON.stringify({ "id": "bson ID", "emailAddress": "fake@fake.com", "dungeon": undefined }))
    .once(JSON.stringify(
      { "id": "dungeon ID", "currentFloor": {
        "floorNumber": 1, "events": [
          { "eventType": "Monster", "data": "Firetoad" }
        ]
      }
    }));

  const div = document.createElement('div');
  ReactDOM.render(
    <MemoryRouter>
      <CoreGame  />
    </MemoryRouter>, div);
  await new Promise(resolve => setTimeout(resolve, 1000));
  expect(div.textContent.includes("Unauthenticated"));
});