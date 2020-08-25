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