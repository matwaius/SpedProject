import axios from "axios"

const api = axios.create({
  baseURL: "https://localhost:7258/api"
})

export default api
