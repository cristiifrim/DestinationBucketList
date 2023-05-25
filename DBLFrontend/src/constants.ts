const DEV_BACKEND_API_URL = "http://localhost:8000/api";

export const BACKEND_API_URL =DEV_BACKEND_API_URL;

export function formatDate(date: Date | string | undefined) {
	return date === null || date === undefined
	? "N/A"
	: new Date(date).toLocaleString()
}

export const getEnumValues = (e: any) => {
	return Object.keys(e)
	  .filter((key) => isNaN(Number(key)))
	  .map((key) => e[key]);
};