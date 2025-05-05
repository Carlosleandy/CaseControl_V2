import { createRouter, createWebHashHistory } from 'vue-router'
import MainView from '../layouts/main/Index.vue'
import Login from '@/modules/auth/ui/login/Login.vue'

import authRepository from '@/modules/auth/repository/mock/mockAuthRepository'
import { useSecurity } from '@/modules/auth/composable/useSecurity'
import { useLanguage } from '@/modules/system/composable/useLanguage'
import securityRouter from '@/modules/security/router/securityRouter'
import caseType from '@/modules/CaseType/router/crmRouter'
import caseStatus from '@/modules/CaseStatus/router/crmRouter'
import recommendationStatus from '@/modules/RecommendationStatus/router/crmRouter'
import receptionMedium from '@/modules/ReceptionMedium/router/crmRouter'
import caseInvestigation from '@/modules/CaseInvestigation/router/crmRouter'
import userlevel from '@/modules/UserLevel/router/crmRouter'
import workinggroup from '@/modules/WorkingGroup/router/crmRouter'
import recommendationtype from '@/modules/RecommendationType/router/crmRouter'
import user from '@/modules/User/router/crmRouter'
import fault from '@/modules/Fault/router/crmRouter'
import evidenceClassification from '@/modules/EvidenceClassification/router/crmRouter'
import linkType from '@/modules/LinkType/router/crmRouter'
import caseAssignment from '@/modules/CaseAssignment/router/crmRouter'

const router = createRouter({
	history: createWebHashHistory(),
	routes: [
		{
			path: '/login',
			name: 'login',
			component: Login,
			props: {
				authRepository
			}
		},

		{
			path: '/',
			name: 'main',
			component: MainView,
			children: [
				{
					path: '/',
					name: 'home',
					component: () => import('../views/Home.vue')
				},
				...securityRouter,
				...caseType,
				...caseStatus,
				...recommendationStatus,
				...receptionMedium,
				...caseInvestigation,
				...userlevel,
				...workinggroup,
				...recommendationtype,
				...user, 
				...fault,
				...evidenceClassification,
				...linkType,
				...caseAssignment,

				{
					path:'/rankingcasesbyuser',
					name:'rankingcasesbyuser',
					component: () => import('@/modules/CaseInvestigation/ui/caseinvestigation/RankingCasesByUser.vue'),
					meta: {
						i18n: ['rankingcasesbyuser']
					},
					props: {
						
					}
				},
				{
					path:'/faultsbylinked',
					name:'faultsbylinked',
					component: () => import('@/modules/CaseInvestigation/ui/caseinvestigation/FaultsByLinked.vue'),
					meta: {
						i18n: ['faultsbylinked']
					},
					props: {
						
					}
				},
				{
					path:'/casesbylinked',
					name:'casesbylinked',
					component: () => import('@/modules/CaseInvestigation/ui/caseinvestigation/CasesByLinked.vue'),
					meta: {
						i18n: ['casesbylinked']
					},
					props: {
						
					}
				},
				{
					path:'/casesbystatussummary',
					name:'casesbystatussummary',
					component: () => import('@/modules/CaseInvestigation/ui/caseinvestigation/CasesByStatusSummary.vue'),
					meta: {
						i18n: ['casesbystatussummary']
					},
					props: {
						
					}
				},
				{
					path:'/casesbyrecommendationtypesummary',
					name:'casesbyrecommendationtypesummary',
					component: () => import('@/modules/Recommendation/ui/recommendation/CasesByRecommendationTypeSummary.vue'),
					meta: {
						i18n: ['casesbyrecommendationtypesummary']
					},
					props: {
						
					}
				},
				{
					path:'/casesbyusernamesummary',
					name:'casesbyusernamesummary',
					component: () => import('@/modules/CaseInvestigation/ui/caseinvestigation/CasesByUserNameSummary.vue'),
					meta: {
						i18n: ['casesbyusernamesummary']
					},
					props: {
						
					}
				},
				{
					path:'/casesrecoverysummary',
					name:'casesrecoverysummary',
					component: () => import('@/modules/CaseInvestigation/ui/caseinvestigation/CasesRecoverySummary.vue'),
					meta: {
						i18n: ['casesrecoverysummary']
					},
					props: {
						
					}
				},
				{
					path:'/role',
					name:'role',
					component: () => import('@/modules/Role/ui/role/Role.vue'),
					meta: {
						i18n: ['roles']
					},
					props: {
						
					}
				},

			]
		},
	]
});

const setRouterSecurity = (app: any) => {
	const { loadLanguageKeys } = useLanguage(app._context.config.globalProperties);

	router.beforeEach((to, from, next) => {
		const { userIsLogged, hasPrivilege } = useSecurity();

		if(!userIsLogged.value && (to.path != '/login'))
			next('/login');        
		else{
			//Avoid to enter login when the user is logged
			if(to?.meta?.i18n) {
				loadLanguageKeys(to.meta.i18n as string[]);
			}

			// Check user privileges to into the requested view
			if(to?.meta?.privileges) {
				if(!hasPrivilege(to?.meta?.privileges as string[])) {
					next('/');
					return;
				}
			}

			if(userIsLogged.value && to.path == '/login') {
				next('/');
				return;
			}
	
			next();
		}
	
		window.scrollTo(0, 0);
	});
};

export const useRouter = ((app: any) => {
	app.use(router);
	setRouterSecurity(app);
});

export default router;
