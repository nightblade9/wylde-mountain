export function isUserAuthenticated()
{
  return localStorage.getItem("userInfo") != null;
}

export async function getCurrentUserAsync()
{
  console.log("2) starting async");
  const headers:Record<string, string> = {
    "Bearer" : localStorage.getItem("userInfo")!
  };

  const response = await fetch('api/User', {
    headers: headers
  });
  
  if (response.ok)
  {
    const data = await response.json();
    console.log("2) Get async: " + JSON.stringify(data));
    return data;
  }
}

export function getCurrentUser()
{
  const headers:Record<string, string> = {
    "Bearer" : localStorage.getItem("userInfo")!
  };

  fetch('api/User', {
    headers: headers
  })
  .then(response =>
  {
    if (response.ok)
    {
      return response.json();
    }
  })
  .then(data =>
  {
    return data;
  });
}