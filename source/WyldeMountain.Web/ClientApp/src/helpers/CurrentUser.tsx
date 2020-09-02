import { IUser } from "../interfaces/IUser";
import jwt_decode from "jwt-decode";

export function isUserAuthenticated()
{
  var token = localStorage.getItem("userInfo");
  if (token == null)
  {
    return false;
  }

  var decoded:any = jwt_decode(token);
  var expiry:number = decoded.exp;
  if (expiry == null)
  {
    return true; // never expires ...
  }

  var now = Date.now() / 1000;
  // Return true if we have a token
  return expiry > now;
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