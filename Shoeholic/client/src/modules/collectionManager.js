import { getToken } from "./authManager";
const _apiUrl = "/api/collection";

export const getUserCollectionByUserId = () => {
    return getToken().then((token) =>
      fetch(`${_apiUrl}/UserCollectionByUser`, {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }).then((res) => {
        if (res.ok) {
          return res.json();
        } else {
          throw new Error("Error");
        }
      })
    );
  };

  export const addCollection = (collection) => {
    return getToken().then((token) =>
      fetch(_apiUrl, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(collection),
      }).then((res) => {
        if (res.ok) {
          return res.json();
        } else {
          throw new Error("Error");
        }
      })
    );
  };