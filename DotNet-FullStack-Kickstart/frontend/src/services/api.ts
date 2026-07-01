export interface TodoItem {
  id: number
  title: string
  description: string
  isCompleted: boolean
  createdAt: string
}

const apiBaseUrl = import.meta.env.VITE_API_BASE_URL ?? 'http://localhost:5214/api/todo'

async function request<T>(path: string, init?: RequestInit): Promise<T> {
  const response = await fetch(`${apiBaseUrl}${path}`, {
    headers: {
      'Content-Type': 'application/json',
      ...(init?.headers ?? {}),
    },
    ...init,
  })

  if (!response.ok) {
    throw new Error(`API request failed with status ${response.status}`)
  }

  if (response.status === 204) {
    return undefined as T
  }

  return (await response.json()) as T
}

export const todoApi = {
  getAll: () => request<TodoItem[]>(''),
  create: (payload: Pick<TodoItem, 'title' | 'description'>) =>
    request<TodoItem>('', {
      method: 'POST',
      body: JSON.stringify({ ...payload, isCompleted: false }),
    }),
  update: (id: number, payload: Pick<TodoItem, 'title' | 'description' | 'isCompleted'>) =>
    request<void>(`/${id}`, {
      method: 'PUT',
      body: JSON.stringify(payload),
    }),
  remove: (id: number) =>
    request<void>(`/${id}`, {
      method: 'DELETE',
    }),
}
