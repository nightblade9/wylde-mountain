import { globalSettings } from '../../App'
import { getCurrentUserAndDungeonAsync, isUserAuthenticated } from '../../helpers/CurrentUser';
import { IDungeonEvent } from '../../interfaces/IDungeon';
import { IUser } from '../../interfaces/IUser';
import React, { useEffect, useState } from 'react';
import LocalizedStrings from 'react-localization';
import { useHistory } from 'react-router';

export const ExploreScene = () =>
{
  
  const [user, setUser] = useState<IUser | undefined>(undefined);
  const [fetchedUser, setFetchedUser] = useState(false);
  const history = useHistory();

  const languageStrings = new LocalizedStrings({
    "en": require('~/../../resources/components/scenes/ExploreScene-en.json'),
    "ar": require('~/../../resources/components/scenes/ExploreScene-ar.json')
  });
  languageStrings.setLanguage(globalSettings.language);

  useEffect(() => {
    const fetchUser = async () => {
      if (!isUserAuthenticated())
      {
        setFetchedUser(true); // don't keep fetching
      }
      else if (!fetchedUser)
      {
        setFetchedUser(true);
        var data = await getCurrentUserAndDungeonAsync();
        setUser(data);
      }
    }

    fetchUser();
  });

  if (user === undefined || user.dungeon === undefined)
  {
    return (
      <div>{languageStrings.loading}</div>
    );
  }
  else
  {
    const finalHtml = [];
    
    for (let i = 0; i < user.dungeon.currentFloor.events.length; i++)
    {
      const eventArray:IDungeonEvent[] = user.dungeon.currentFloor.events[i];
      const event:IDungeonEvent = eventArray[0]; // or null
      
      if (event != null) {
        finalHtml.push(<li key={"choice" + i} >
          {event.eventType}: {event.data}
            <button onClick={e => {
              history.push("/battle?choice=" + i);
            }}>interact</button>
        </li>);
      } else {
        finalHtml.push(<li key={"choice" + i} >(empty)</li>);
      }
    }
    return (
        <div>
          <p>
            {languageStrings.formatString(languageStrings.floorIndicator,
            {
              "floorNumber": user.dungeon.currentFloor.floorNumber
            })}
          </p>
          <p>
            {languageStrings.youAre}
            &nbsp;
            <strong>
              {languageStrings.formatString(languageStrings.characterAndLevel,
                {
                  "level": user.level,
                  "character": user.character
                })}
            </strong>
            &nbsp;
            {languageStrings.formatString(languageStrings.healthAndSkillPoints,
            {
              "hp": user.currentHealthPoints,
              "maxHp": user.maxHealthPoints,
              "sp": user.currentSkillPoints,
              "maxSp": user.maxSkillPoints
            })}
          </p>
          <ol>
            {finalHtml}
          </ol>
        </div>
    );
  }
}