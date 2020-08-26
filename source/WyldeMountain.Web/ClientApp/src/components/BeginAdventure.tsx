import { globalSettings } from '../App';
import React, { useEffect } from 'react';
import LocalizedStrings from 'react-localization';

const BeginAdventure = (props:any) => {
    const languageStrings = new LocalizedStrings({
        "en": require('~/../../resources/components/BeginAdventure-en.json'),
        "ar": require('~/../../resources/components/BeginAdventure-ar.json')
    });
    languageStrings.setLanguage(globalSettings.language);
        
    useEffect(() => {
        const generateCall = async () => {
            const headers:Record<string, string> = {
                "Bearer" : localStorage.getItem("userInfo") || ""
            };
        
            const response = await fetch('api/DungeonGenerator', {
                headers: headers,
                method: "POST"
            });
            
            if (response.ok)
            {
                props.history.push("/core-game");
            }
        }

        generateCall();
    });

    return (
        <div>
            <h1>{languageStrings.generatingAdventure}</h1>
        </div>
    )
}

export default BeginAdventure;