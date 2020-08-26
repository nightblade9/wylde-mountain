import React, { useEffect } from 'react';
import { Redirect } from 'react-router';

const BeginAdventure = (props:any) => {
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
                return (
                    <Redirect to="/core-game" />
                );
            }
        }

        generateCall();
    });

    return (
        <div>
            <h1>Generating adventure ...</h1>
        </div>
    )
}

export default BeginAdventure;