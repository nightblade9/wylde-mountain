import { IUser } from "../interfaces/IUser";

export function isUserAuthenticated()
{
  return localStorage.getItem("userInfo") != null;
}

export async function getCurrentUserAsync()
{
  const headers:Record<string, string> = {
    "Bearer" : localStorage.getItem("userInfo") || ""
  };

  const response = await fetch('api/User', {
    headers: headers
  });
  
  if (response.ok)
  {
    const data = await response.json();
    return data;
  }
}


export async function getCurrentUserAndDungeonAsync()
{
  var user:IUser = await getCurrentUserAsync();
  const headers:Record<string, string> = {
    "Bearer" : localStorage.getItem("userInfo") || ""
  };

  const response = await fetch('api/Dungeon', {
    headers: headers
  });
  
  if (response.ok)
  {
    const data = await response.json();
    user.dungeon = data;
    return user;
  }
}