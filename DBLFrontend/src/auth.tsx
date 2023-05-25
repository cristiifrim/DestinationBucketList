import { User, Role } from "./models/User";
import jwt_decode from "jwt-decode";
import { useNavigate } from "react-router-dom";
import { SnackbarContext } from "./components/SnackbarContext";
import { useContext } from "react";

interface JwtPayload {
  exp: number;
}

let token: string | null = null;
let tokenExpirationDate: number | null = null;
let account: User | null = null;

export const logOut = () => {
  setAuthToken(null);
  setAccount(null);
};

export const isAuthorized = (userId: number | undefined) => {
  return (
    account !== null &&
    account.role !== undefined &&
    (account.role > Role.user || account.id === userId)
  );
};

export const setAuthToken = (newToken: string | null) => {
  token = newToken;
  if (!token) return;

  const decodedToken = jwt_decode<JwtPayload>(token);
  const expirationDate = decodedToken.exp * 1000;

  console.log("Token expiration date:", new Date(expirationDate));
  tokenExpirationDate = expirationDate;
};

export const getAuthToken = () => {
  //const navigate = useNavigate();
  //const openSnackbar = useContext(SnackbarContext);
  if (token && tokenExpirationDate && tokenExpirationDate < Date.now()) {
    logOut();

    //openSnackbar("error", "Permission denied!");
    //navigate("/users/login");
  }

  return token;
};

export const setAccount = (newAccount: User | null) => {
  account = newAccount;
};

export const getAccount = () => {
  return account;
};

export const updatePref = (userId: number | undefined, pref: number) => {
  if (account && account.userProfile && account.id === userId) {
    account.userProfile.pagePreference = pref;
  }
};
