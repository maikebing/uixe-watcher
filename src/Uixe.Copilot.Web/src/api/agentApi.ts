export interface AgentCommandRequest {
  commandType: 'notification' | 'speech' | 'vnc' | 'web'
  title?: string
  message?: string
  text?: string
  voiceName?: string
  volume?: number
  rate?: number
  playSpeech?: boolean
  host?: string
  port?: number
  password?: string
  vncTitle?: string
  url?: string
  webTitle?: string
  width?: number
  height?: number
  keepRunning?: boolean
}

export interface AgentCommandResponse {
  success: boolean
  message: string
}

const agentBaseUrl = import.meta.env.VITE_AGENT_BASE_URL ?? 'http://127.0.0.1:17173'

export async function postAgentCommand(payload: AgentCommandRequest): Promise<AgentCommandResponse> {
  const response = await fetch(`${agentBaseUrl}/commands`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({
      keepRunning: true,
      ...payload
    })
  })

  const data = (await response.json()) as AgentCommandResponse
  if (!response.ok) {
    throw new Error(data.message || 'Agent µ÷ÓÃÊ§°Ü')
  }

  return data
}

export function notifyByAgent(title: string, message: string, options?: Pick<AgentCommandRequest, 'playSpeech' | 'text' | 'voiceName' | 'volume' | 'rate'>) {
  return postAgentCommand({
    commandType: 'notification',
    title,
    message,
    ...options
  })
}

export function speakByAgent(text: string, options?: Pick<AgentCommandRequest, 'voiceName' | 'volume' | 'rate'>) {
  return postAgentCommand({
    commandType: 'speech',
    text,
    ...options
  })
}

export function openVncByAgent(host: string, port = 5900, password?: string, vncTitle?: string) {
  return postAgentCommand({
    commandType: 'vnc',
    host,
    port,
    password,
    vncTitle
  })
}

export function openWebByAgent(url: string, webTitle?: string) {
  return postAgentCommand({
    commandType: 'web',
    url,
    webTitle
  })
}