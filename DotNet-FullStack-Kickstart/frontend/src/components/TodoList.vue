<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { todoApi, type TodoItem } from '../services/api'

const todos = ref<TodoItem[]>([])
const title = ref('')
const description = ref('')
const isLoading = ref(false)
const errorMessage = ref('')

const loadTodos = async () => {
  isLoading.value = true
  errorMessage.value = ''

  try {
    todos.value = await todoApi.getAll()
  } catch {
    errorMessage.value = '載入待辦失敗，請確認後端 API 是否已啟動。'
  } finally {
    isLoading.value = false
  }
}

const addTodo = async () => {
  const trimmedTitle = title.value.trim()
  if (!trimmedTitle) {
    return
  }

  await todoApi.create({
    title: trimmedTitle,
    description: description.value.trim(),
  })

  title.value = ''
  description.value = ''
  await loadTodos()
}

const toggleTodo = async (item: TodoItem) => {
  await todoApi.update(item.id, {
    title: item.title,
    description: item.description,
    isCompleted: !item.isCompleted,
  })
  await loadTodos()
}

const deleteTodo = async (id: number) => {
  await todoApi.remove(id)
  await loadTodos()
}

onMounted(loadTodos)
</script>

<template>
  <div class="mx-auto max-w-3xl rounded-2xl bg-white p-6 shadow-lg">
    <h1 class="mb-6 text-2xl font-bold text-slate-800">Todo List</h1>

    <div class="mb-6 grid gap-3">
      <input
        v-model="title"
        type="text"
        placeholder="任務標題"
        class="w-full rounded-lg border border-slate-300 px-4 py-2 outline-none transition focus:border-blue-500"
      />
      <input
        v-model="description"
        type="text"
        placeholder="任務描述（可選）"
        class="w-full rounded-lg border border-slate-300 px-4 py-2 outline-none transition focus:border-blue-500"
      />
      <button
        type="button"
        class="rounded-lg bg-blue-600 px-4 py-2 font-medium text-white transition hover:bg-blue-700 disabled:cursor-not-allowed disabled:opacity-50"
        :disabled="!title.trim()"
        @click="addTodo"
      >
        新增
      </button>
    </div>

    <p v-if="errorMessage" class="mb-4 text-sm text-red-600">{{ errorMessage }}</p>
    <p v-else-if="isLoading" class="mb-4 text-sm text-slate-500">載入中...</p>

    <ul class="space-y-3">
      <li
        v-for="todo in todos"
        :key="todo.id"
        class="flex items-center justify-between rounded-lg border border-slate-200 px-4 py-3"
      >
        <div>
          <p :class="['font-medium', todo.isCompleted ? 'line-through text-slate-400' : 'text-slate-800']">
            {{ todo.title }}
          </p>
          <p class="text-sm text-slate-500">{{ todo.description }}</p>
        </div>

        <div class="flex gap-2">
          <button
            type="button"
            class="rounded-md bg-emerald-500 px-3 py-1 text-sm text-white hover:bg-emerald-600"
            @click="toggleTodo(todo)"
          >
            {{ todo.isCompleted ? '取消完成' : '勾選完成' }}
          </button>
          <button
            type="button"
            class="rounded-md bg-rose-500 px-3 py-1 text-sm text-white hover:bg-rose-600"
            @click="deleteTodo(todo.id)"
          >
            刪除
          </button>
        </div>
      </li>
    </ul>
  </div>
</template>
